import { Routes } from '@angular/router';
import { LogIn } from './modules/auth/log-in/log-in';
import { SignUp } from './modules/auth/sign-up/sign-up';
import { IncomeHome } from './modules/income-source/income-home/income-home';
import { IncomeDetails } from './modules/income-source/income-details/income-details';
import { InTransactHome } from './modules/income-transaction/in-transact-home/in-transact-home';
import { ExpenseHome } from './modules/expense-source/expense-home/expense-home';
import { OutTransactHome } from './modules/expense-transaction/out-transact-home/out-transact-home';

export const routes: Routes = [
  // defualt root path
  { path: '', redirectTo: 'income-home', pathMatch: 'full' },

  // auth routes
  { path: 'log-in', component: LogIn },
  { path: 'sign-up', component: SignUp },

  // income routes
  { path: 'income-home', component: IncomeHome },

  // income transaction routes
  { path: 'in-transact-home', component: InTransactHome },

  // expense routes
  { path: 'expense-home', component: ExpenseHome },

  // expense transaction routes
  { path: 'out-transact-home', component: OutTransactHome },
];