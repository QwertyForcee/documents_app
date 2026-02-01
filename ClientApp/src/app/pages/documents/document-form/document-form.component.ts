import { CommonModule } from '@angular/common';
import { Component, Inject, Input, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CreateDocumentModel, DocumentDetails } from '../../../models/documents-models';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { iif, of, Subject, takeUntil } from 'rxjs';
import { CommentsComponent } from '../comments/comments.component';
import { Clipboard } from '@angular/cdk/clipboard';
import { DocumentFormData } from '../../../models/document-form-data';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-document-form',
  standalone: true,
  imports: [
    MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, MatDialogModule, CommonModule, CommentsComponent, MatDatepickerModule
  ],
  templateUrl: './document-form.component.html',
  styleUrl: './document-form.component.scss'
})
export class DocumentFormComponent implements OnDestroy {
  name: string = '';
  description: string = '';
  jobTitle: string = '';
  expirationDate!: Date;
  latitude?: number;
  longitude?: number;

  isReadOnly?: boolean;
  documentId?: string;
  private readonly destroy$ = new Subject<void>();

  get isNew(): boolean {
    return !this.documentId;
  }

  get formTitle() {
    return !this.isNew ? 'Change document' : 'Create document';
  }

  constructor(
    private dialogRef: MatDialogRef<DocumentFormComponent>,
    private clipboard: Clipboard,
    @Inject(MAT_DIALOG_DATA) public data: DocumentFormData | null
  ) {
    this.isReadOnly = data?.isReadOnly;
    const documentDetails = data?.documentDetails;

    if (documentDetails) {
      this.documentId = documentDetails.id;

      this.name = documentDetails.name;
      this.description = documentDetails.description;
      this.latitude = documentDetails.latitude;
      this.longitude = documentDetails.longitude;
      if (documentDetails.expirationDate) {
        this.expirationDate = new Date(documentDetails.expirationDate);
      }
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  save(form: NgForm) {
    if (!form.valid) { return; }

    const utcDate = new Date(Date.UTC(
      this.expirationDate.getFullYear(),
      this.expirationDate.getMonth(),
      this.expirationDate.getDate(),
      0, 0, 0
    ));

    const isoUTC = utcDate.toISOString();

    const newContact: CreateDocumentModel = {
      name: this.name,
      description: this.description,
      latitude: this.latitude,
      longitude: this.longitude,
      expirationDate: isoUTC
    };

    this.dialogRef.close(newContact);
  }

  copyLink(): void {
    const baseUrl = window.location.origin;
    const link = `${baseUrl}/docs/${this.documentId}`;

    this.clipboard.copy(link);
  }
}
