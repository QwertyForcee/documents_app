import { Component, OnDestroy, OnInit } from '@angular/core';
import { DocumentDetails, DocumentListItem, DocumentStatus } from '../../../models/documents-models';
import { DocumentsService } from '../../../services/documents.service';
import { CommonModule } from '@angular/common';
import { filter, forkJoin, map, of, Subject, switchMap, take, takeUntil, tap } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { DocumentFormComponent } from '../document-form/document-form.component';
import { DocumentCardComponent } from '../document-card/document-card.component';
import { ActivatedRoute } from '@angular/router';
import { DocumentFormData } from '../../../models/document-form-data';

@Component({
  selector: 'app-documents-page',
  standalone: true,
  imports: [
    CommonModule,
    DocumentCardComponent
  ],
  templateUrl: './documents-page.component.html',
  styleUrl: './documents-page.component.scss'
})
export class DocumentsPageComponent implements OnInit, OnDestroy {
  isReadOnly = false;
  private documentStatus: DocumentStatus = DocumentStatus.Active;

  documents: DocumentListItem[] = [];
  private readonly destroy$ = new Subject<void>();

  constructor(
    private dialog: MatDialog,
    private service: DocumentsService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.route.data
      .pipe(
        tap(data => {
          this.isReadOnly = data['isReadOnly'];
          this.documentStatus = data['documentStatus'];
        }),
        switchMap(() => this.service.getDocuments(this.documentStatus)),
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (res) => {
          this.documents = res;
        }
      })

    this.route.paramMap
      .pipe(
        map((params) => params.get('id')),
        filter((documentId) => !!documentId && !this.isReadOnly),
        switchMap((documentId) => this.service.copyDocument(documentId!)),
        filter((newDocumentId) => !!newDocumentId),
        switchMap(newDocumentId =>
          forkJoin({
            newDocumentId: of(newDocumentId),
            documents: this.service.getDocuments(this.documentStatus)
          })
        ),
        takeUntil(this.destroy$),
      )
      .subscribe(({ newDocumentId, documents }) => {
        this.documents = documents;
        this.onOpenForm(newDocumentId);
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onAddDocumentClick(): void {
    const dialogRef = this.dialog.open(DocumentFormComponent, { width: '400px' });

    dialogRef.afterClosed()
      .pipe(
        filter((result) => !!result),
        switchMap(result => this.service.createDocument(result)),
        switchMap(() => this.service.getDocuments(this.documentStatus)),
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (result) => {
          this.documents = result;
        },
        error: (err) => {
          console.error(err);
        }
      });
  }

  onOpenForm(documentId: string): void {
    this.service.getDocumentById(documentId)
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe({
        next: (docDetails) => {
          this.openDocumentForm(docDetails);
        }
      })
  }

  private openDocumentForm(docDetails: DocumentDetails): void {
    const data = { isReadOnly: this.isReadOnly, documentDetails: docDetails } as DocumentFormData;
    const dialogRef = this.dialog.open(DocumentFormComponent, { width: '400px', data: data });

    dialogRef.afterClosed()
      .pipe(
        filter((res) => res && !this.isReadOnly),
        switchMap(updated => {
          return this.service.updateDocument(docDetails.id, updated);
        }),
        switchMap(() => this.service.getDocuments(this.documentStatus))
      )
      .subscribe({
        next: list => this.documents = list
      });
  }

  onDelete(contact: DocumentListItem): void {
    if (!contact.id) return;

    this.service.deleteDocument(contact.id)
      .pipe(
        switchMap(() => this.service.getDocuments(this.documentStatus))
      )
      .subscribe({
        next: (list) => {
          this.documents = list;
        }
      });
  }
}
