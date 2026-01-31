import { Component, OnDestroy, OnInit } from '@angular/core';
import { DocumentDetails, DocumentListItem } from '../../../models/documents-models';
import { DocumentsService } from '../../../services/documents.service';
import { CommonModule } from '@angular/common';
import { filter, Subject, switchMap, takeUntil } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { DocumentFormComponent } from '../document-form/document-form.component';
import { DocumentCardComponent } from '../document-card/document-card.component';

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
  documents: DocumentListItem[] = [];
  private readonly destroy$ = new Subject<void>();

  constructor(
    private dialog: MatDialog,
    private service: DocumentsService
  ) { }

  ngOnInit(): void {
    this.service.getDocuments()
      .pipe(
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (res) => {
          this.documents = res;
        }
      })
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
        switchMap(() => this.service.getDocuments()),
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

  onEdit(document: DocumentListItem): void {
    this.service.getDocumentById(document.id)
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
    const dialogRef = this.dialog.open(DocumentFormComponent, { width: '400px', data: docDetails });
    dialogRef.afterClosed()
      .pipe(
        filter(Boolean),
        switchMap(updated => {
          return this.service.updateDocument(docDetails.id, updated);
        }),
        switchMap(() => this.service.getDocuments())
      )
      .subscribe({
        next: list => this.documents = list
      });
  }

  onDelete(contact: DocumentListItem): void {
    if (!contact.id) return;

    this.service.deleteDocument(contact.id)
      .pipe(
        switchMap(() => this.service.getDocuments())
      )
      .subscribe({
        next: (list) => {
          this.documents = list;
        }
      });
  }
}
