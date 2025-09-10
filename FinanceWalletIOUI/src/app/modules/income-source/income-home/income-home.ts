import { Component } from '@angular/core';
import { IncomeCreate } from "../income-create/income-create";
import { IncomeUpdate } from "../income-update/income-update";
import { IncomeDetails } from "../income-details/income-details";

@Component({
  selector: 'app-income-home',
  imports: [IncomeCreate, IncomeUpdate, IncomeDetails],
  templateUrl: './income-home.html',
  styleUrl: './income-home.css'
})
export class IncomeHome {

}
