import { CommonModule } from '@angular/common';
import { Component, Inject, Input, OnDestroy, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CreateDocumentModel, DocumentDetails } from '../../../models/documents-models';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommentModel, CreateCommentModel } from '../../../models/comment-model';
import { CommentsService } from '../../../services/comments.service';
import { iif, of, Subject, takeUntil } from 'rxjs';
import { CommentsComponent } from '../comments/comments.component';

@Component({
  selector: 'app-document-form',
  standalone: true,
  imports: [
    MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, MatDialogModule, CommonModule, CommentsComponent
  ],
  templateUrl: './document-form.component.html',
  styleUrl: './document-form.component.scss'
})
export class DocumentFormComponent implements OnDestroy {
  name: string = '';
  description: string = '';
  jobTitle: string = '';
  expirationDate: Date | undefined;
  latitude?: number;
  longitude?: number;

  documentId?: string;
  private readonly destroy$ = new Subject<void>();

  get formTitle() {
    return !!this.documentId ? 'Create document' : 'Change document';
  }

  constructor(
    private dialogRef: MatDialogRef<DocumentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DocumentDetails | null
  ) {
    if (data) {
      this.documentId = data.id;

      this.name = data.name;
      this.description = data.description;
      this.latitude = data.latitude;
      this.longitude = data.longitude;
      if (data.expirationDate) {
        this.expirationDate = new Date(data.expirationDate);
      }
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  save(form: NgForm) {
    if (!form.valid) { return; }

    const newContact: CreateDocumentModel = {
      name: this.name,
      description: this.description,
      latitude: this.latitude,
      longitude: this.longitude,
      expirationDate: this.expirationDate!.toISOString()
    };

    this.dialogRef.close(newContact);
  }

  onExpirationDateChanged($event: Date | null): void {
    this.expirationDate = $event ? new Date($event) : undefined
  }
}
