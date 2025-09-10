import { Component } from '@angular/core';
import { InTransactCreate } from "../in-transact-create/in-transact-create";
import { InTransactDetails } from "../in-transact-details/in-transact-details";
import { InTransactUpdate } from "../in-transact-update/in-transact-update";

@Component({
  selector: 'app-in-transact-home',
  imports: [InTransactCreate, InTransactDetails, InTransactUpdate],
  templateUrl: './in-transact-home.html',
  styleUrl: './in-transact-home.css'
})
export class InTransactHome {

}
