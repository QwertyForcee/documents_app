import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DocumentListItem } from '../../../models/documents-models';

@Component({
  selector: 'app-document-card',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './document-card.component.html',
  styleUrl: './document-card.component.scss'
})
export class DocumentCardComponent {
  @Input() document!: DocumentListItem;
  @Input() isReadOnly!: boolean;

  @Output() openForm = new EventEmitter<DocumentListItem>();
  @Output() delete = new EventEmitter<DocumentListItem>();

  onOpenForm(): void {
    this.openForm.emit(this.document);
  }

  onDelete(): void {
    if (this.isReadOnly) return;
    this.delete.emit(this.document);
  }
}
