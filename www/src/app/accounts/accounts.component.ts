import { takeUntil } from 'rxjs';
import { AccountsService } from 'app/common/services';
import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DestroyableComponent } from 'app/common/components/destroyable';
import { UserRole } from 'app/common/models';
import { LazyLoadedDialogService } from 'app/common/services/lazy-loaded-dialog.service';

export interface Account {
  id: string;
  username: string;
  userRole: UserRole;
  department: string;
}

@Component({
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatTooltipModule,
  ],
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss'],
})
export default class AccountsComponent
  extends DestroyableComponent
  implements OnInit
{
  displayedColumns: string[] = [
    'id',
    'username',
    'department',
    'userRole',
    'actions',
  ];
  dataSource: MatTableDataSource<Account>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _lazyDialog: LazyLoadedDialogService,
    private _accountsService: AccountsService
  ) {
    super();
  }

  ngOnInit(): void {
    this._accountsService.getAllAccounts().subscribe((accounts) => {
      this.dataSource = new MatTableDataSource(accounts);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event) {
    this.dataSource.filter = (event.target as HTMLInputElement).value
      ?.trim()
      .toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  addAccount() {
    this._lazyDialog
      .openDialog(import('app/dialogs/add-account/add-account.component'))
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((createdAccount: Account) => {
            if (!createdAccount) return;

            this.dataSource.data.push(createdAccount);

            // trigger table change
            this.dataSource.data = this.dataSource.data;
          })
      );
  }

  deleteAccount(account: Account) {
    this._lazyDialog
      .openDialog(
        import('app/dialogs/delete-account/delete-account.component'),
        {
          data: account,
        }
      )
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((deleted) => {
            if (!deleted) return;
            this.dataSource.data = this.dataSource.data.filter(
              (x) => x.id !== account.id
            );
          })
      );
  }
}
