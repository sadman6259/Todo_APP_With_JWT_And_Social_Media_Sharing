import { UserService } from "./../shared/user.service";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { TodoItems } from "../shared/todo-items.model";
import { TodoItemService } from "../shared/todo-items.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent implements OnInit {
  userDetails;
  title = "todo-items";
  id = "todo-list";
  editDisplay = false;
  showEditButton = true;
  IsEdit: boolean = false;
  IsProgress: boolean = false;
  //imports todo class as a Type
  todos: TodoItems[] = [];
  todo: any;
  todoName: any = "404";
  randomData: any = {};
  inputTodo: string = "";
  inputTodoprogress: number;
  inputTodoSecret: string = "";
  inputTodoId: number;
  progresspercentage: number;
  totalpendingtask: number;
  minh: number;
  max: number;
  val: number;

  public link: string = "";

  constructor(
    private router: Router,
    private service: UserService,
    private api: TodoItemService,
    private randomApi: TodoItemService
  ) {}

  ngOnInit() {
    this.api.refreshList().subscribe((data: any) => {
      this.todos = data;
      this.todos.forEach((element) => {
        if (element.progressPercentage == 100) {
          element.TodoStatus = "completed";
          element.TwitterStatus =
            "I have successfully completed " + element.todoName;
        } else if (
          element.progressPercentage < 100 &&
          element.progressPercentage > 0
        ) {
          element.TodoStatus = "running";
        } else if (element.progressPercentage == 0) {
          element.TodoStatus = "not started";
        }
      });
      this.totalpendingtask = this.todos.filter(
        (x) => x.progressPercentage < 100
      ).length;
      debugger;
    });

    this.randomApi.apiCall().subscribe((data: any) => {
      this.randomData = data;
    });
  }

  //opens edit component
  editTodo(id: number, name: any) {
    this.inputTodoId = id;
    this.inputTodo = name;
    this.IsEdit = true;
  }
  CancelEdit() {
    this.IsEdit = false;
    this.IsProgress = false;
    this.inputTodo = "";
  }
  // Deletes an item from the list
  deleteTodo(id: number) {
    debugger;
    this.todos = this.todos.filter((value, i) => i !== id);
    this.api.deleteTodo(id).subscribe((data: any) => {
      this.api.refreshList().subscribe((data: any) => {
        this.todos = data;
        this.todos.forEach((element) => {
          if (element.progressPercentage == 100) {
            element.TodoStatus = "completed";
            element.TwitterStatus =
              "I have successfully completed " + element.todoName;
          } else if (
            element.progressPercentage < 100 &&
            element.progressPercentage > 0
          ) {
            element.TodoStatus = "running";
          } else if (element.progressPercentage == 0) {
            element.TodoStatus = "not started";
          }
        });
        this.totalpendingtask = this.todos.filter(
          (x) => x.progressPercentage < 100
        ).length;
      });
    });
  }

  // Deletes all todos from list
  deleteAll() {
    this.editDisplay = false;
    this.todos = [];
    this.api.deleteAllTodo().subscribe((data: any) => {
      this.api.refreshList().subscribe((data: any) => {
        this.todos = data;
        this.todos.forEach((element) => {
          if (element.progressPercentage == 100) {
            element.TodoStatus = "completed";
            element.TwitterStatus =
              "I have successfully completed " + element.todoName;
          } else if (
            element.progressPercentage < 100 &&
            element.progressPercentage > 0
          ) {
            element.TodoStatus = "running";
          } else if (element.progressPercentage == 0) {
            element.TodoStatus = "not started";
          }
        });
        this.totalpendingtask = this.todos.filter(
          (x) => x.progressPercentage < 100
        ).length;
      });
    });
  }

  progressTodo(id: number, progrespercentage: number) {
    this.progresspercentage = progrespercentage;
    this.inputTodoprogress = progrespercentage;
    this.IsProgress = true;
    this.IsEdit = false;
    this.inputTodoId = id;
  }
  // Pushes a new todo to state and api, clears form
  addTodo() {
    this.showEditButton = true;
    if (this.inputTodo === "") {
      return null;
    } else {
      this.todo = { TodoName: this.inputTodo };
      this.api.addTodo(this.todo).subscribe((data: any) => {
        this.api.refreshList().subscribe((data: any) => {
          this.todos = data;
          this.todos.forEach((element) => {
            if (element.progressPercentage == 100) {
              element.TodoStatus = "completed";
              element.TwitterStatus =
                "I have successfully completed " + element.todoName;
            } else if (
              element.progressPercentage < 100 &&
              element.progressPercentage > 0
            ) {
              element.TodoStatus = "running";
            } else if (element.progressPercentage == 0) {
              element.TodoStatus = "not started";
            }
          });
          this.totalpendingtask = this.todos.filter(
            (x) => x.progressPercentage < 100
          ).length;
        });
      });

      //clear input after submit
      this.inputTodo = "";
      return "pushed to list";
    }
  }

  updateTodo(id: any, secret: any) {
    debugger;
    //this.hideSubmit.emit(!this.hideSubmit);
    if (this.inputTodo !== "") {
      let todo = { TodoName: this.inputTodo, TodoSecret: secret };
      this.api.updateTodo(id, todo).subscribe((data: any) => {
        this.api.refreshList().subscribe((data: any) => {
          this.todos = data;
          this.todos.forEach((element) => {
            if (element.progressPercentage == 100) {
              element.TodoStatus = "completed";
              element.TwitterStatus =
                "I have successfully completed " + element.todoName;
            } else if (
              element.progressPercentage < 100 &&
              element.progressPercentage > 0
            ) {
              element.TodoStatus = "running";
            } else if (element.progressPercentage == 0) {
              element.TodoStatus = "not started";
            }
          });
          this.totalpendingtask = this.todos.filter(
            (x) => x.progressPercentage < 100
          ).length;
        });
        this.inputTodo = "";
        this.IsEdit = false;
      });
    } else {
      alert("Please provide a todo value");
    }
  }
  CalculateProgress(event) {
    this.progresspercentage = event.target.value;
    debugger;
  }

  updateTodoProgress(id: any, progress: any) {
    debugger;
    var a = this.progresspercentage;
    if (this.progresspercentage !== 0) {
      let todo = {
        TodoName: this.inputTodo,
        ProgressPercentage: this.progresspercentage,
      };
      this.api.updateTodoProgress(id, todo).subscribe((data: any) => {
        this.IsProgress = false;
        this.IsEdit = false;
        this.inputTodo = "";
        this.api.refreshList().subscribe((data: any) => {
          this.todos = data;
          this.todos.forEach((element) => {
            if (element.progressPercentage == 100) {
              element.TodoStatus = "completed";
              element.TwitterStatus =
                "I have successfully completed " + element.todoName;
            } else if (
              element.progressPercentage < 100 &&
              element.progressPercentage > 0
            ) {
              element.TodoStatus = "running";
            } else if (element.progressPercentage == 0) {
              element.TodoStatus = "not started";
            }
          });
          this.totalpendingtask = this.todos.filter(
            (x) => x.progressPercentage < 100
          ).length;
        });
      });
    } else {
      alert("Please provide a todo value");
    }
  }

  randomInt() {
    return Math.floor(Math.random() * 100);
  }

  closeSubmit() {
    this.showEditButton = false;
    this.editDisplay = !this.editDisplay;
  }
  onLogout() {
    localStorage.removeItem("token");
    this.router.navigate(["/user/login"]);
  }
}
