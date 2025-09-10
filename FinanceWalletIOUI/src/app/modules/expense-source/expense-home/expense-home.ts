import { Component } from '@angular/core';
import { ExpenseCreate } from "../expense-create/expense-create";
import { ExpenseDetails } from "../expense-details/expense-details";
import { ExpenseUpdate } from "../expense-update/expense-update";

@Component({
  selector: 'app-expense-home',
  imports: [ExpenseCreate, ExpenseDetails, ExpenseUpdate],
  templateUrl: './expense-home.html',
  styleUrl: './expense-home.css'
})
export class ExpenseHome {

}
