export interface CommentModel {
    id: string;
    text: string;
    createdAt: string;
    userId: string;
    userName: string;
} 

export interface CreateCommentModel {
    text: string;
    documentId: string;
}