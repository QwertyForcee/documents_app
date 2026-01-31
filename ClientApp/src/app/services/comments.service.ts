import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommentModel, CreateCommentModel } from '../models/comment-model';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  private apiUrl = 'http://localhost:5000/api/comments';

  constructor(private http: HttpClient) { }

  getComments(documentId: string): Observable<CommentModel[]> {
    return this.http.get<CommentModel[]>(`${this.apiUrl}/${documentId}`);
  }

  createComment(comment: CreateCommentModel): Observable<CommentModel> {
    return this.http.post<CommentModel>(this.apiUrl, comment);
  }

  deleteComment(documentId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${documentId}`);
  }
}
