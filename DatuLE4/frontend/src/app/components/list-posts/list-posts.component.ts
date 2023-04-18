import { Component } from '@angular/core';
import { Post } from 'src/app/models/post.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-list-posts',
  templateUrl: './list-posts.component.html',
  styleUrls: ['./list-posts.component.css']
})
export class ListPostsComponent {
  posts?: Post[] = [];
  constructor(private http: HttpClient){}
  
  ngOnInit(): void{
    this.initData();
  }
  
  initData(): void{
    this.http.get<Post[]>('https://localhost:7000/api/Post/listPosts')
    .subscribe({
      next: (data: Post[])=> {
        this.posts = data;
        console.log(this.posts)
      }
    })
  }
}
