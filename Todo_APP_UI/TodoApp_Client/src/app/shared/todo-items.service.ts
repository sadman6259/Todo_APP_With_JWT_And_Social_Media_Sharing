import { TodoItems } from "./todo-items.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IfStmt } from "@angular/compiler";

//Front end CRUD methods
@Injectable({
  providedIn: "root",
})
export class TodoItemService {
  formData: TodoItems = new TodoItems();
  readonly APIUrl = "https://todoappcicd.azurewebsites.net/api/";
  list: any;

  constructor(private http: HttpClient) {}

  getTodoList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + "/todoitems/getall");
  }

  addTodo(val: any) {
    return this.http.post(this.APIUrl + "/todoitems", val);
  }

  updateTodo(id: number, val: any) {
    return this.http.put(this.APIUrl + "/todoitems/todoitemsname/" + id, val);
  }

  updateTodoProgress(id: number, val: any) {
    return this.http.put(
      this.APIUrl + "/todoitems/todoitemsprogress/" + id,
      val
    );
  }

  deleteTodo(id: any) {
    return this.http.delete(this.APIUrl + `/todoitems/delete/${id}`);
  }
  deleteAllTodo() {
    return this.http.delete(this.APIUrl + `/todoitems/todoitemsdeleteall/`);
  }
  //Populates existing records into list property.
  refreshList() {
    return this.http.get(this.APIUrl + "/todoitems/getall");
  }

  //Random api call (this one is not using async/await)
  apiCall() {
    return this.http.get("https://jsonplaceholder.typicode.com/posts");
  }
}
