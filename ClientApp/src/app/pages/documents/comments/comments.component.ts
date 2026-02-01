import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { CommentModel, CreateCommentModel } from '../../../models/comment-model';
import { CommentsService } from '../../../services/comments.service';
import { iif, of, Subject, takeUntil } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-comments',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './comments.component.html',
  styleUrl: './comments.component.scss'
})
export class CommentsComponent implements OnInit, OnDestroy {
  @Input() documentId?: string;
  @Input() isReadOnly?: boolean;

  comments: CommentModel[] = [];
  newComment = '';
  private readonly destroy$ = new Subject<void>();

  constructor(private commentsService: CommentsService) { }

  ngOnInit(): void {
    iif(
      () => !!this.documentId,
      this.commentsService.getComments(this.documentId!),
      of([])
    )
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe({
        next: (comments) => {
          this.comments = comments;
        }
      })
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  addComment(): void {
    const text = this.newComment.trim();
    if (!text) return;

    var model = {
      text,
      documentId: this.documentId
    } as CreateCommentModel;

    this.commentsService.createComment(model)
      .subscribe(comment => {
        this.comments.unshift(comment);
        this.newComment = '';
      });
  }

  onDelete(commentId: string): void {
    this.commentsService.deleteComment(commentId)
      .pipe(
        takeUntil(this.destroy$),
      )
      .subscribe({
        next: () => {
          const index = this.comments.findIndex(comment => comment.id === commentId);

          if (index !== -1) {
            this.comments.splice(index, 1);
          }
        }
      })
  }
}
