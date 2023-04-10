import { takeUntil } from 'rxjs';
import { Account } from 'app/accounts/accounts.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { DestroyableComponent } from 'app/common/components/destroyable';
import { AccessLevel } from 'app/common/models';
import { LazyLoadedDialogService } from 'app/common/services/lazy-loaded-dialog.service';
import { AccountsService, CardsService } from 'app/common/services';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';

export interface AccessCard {
  id: string;
  passCode: string;
  userId: string;
  accessLevel: AccessLevel;
  description: string;
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
  templateUrl: './access-cards.component.html',
  styleUrls: ['./access-cards.component.scss'],
})
export default class AccessCardsComponent
  extends DestroyableComponent
  implements OnInit
{
  private _accounts: Account[];

  displayedColumns: string[] = [
    'id',
    'userId',
    'accessLevel',
    'passCode',
    'description',
    'actions',
  ];
  dataSource: MatTableDataSource<AccessCard>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _lazyDialog: LazyLoadedDialogService,
    private _cardsService: CardsService,
    private _accountsService: AccountsService
  ) {
    super();
  }

  ngOnInit(): void {
    this._cardsService.getAllCards().subscribe((cards) => {
      this.dataSource = new MatTableDataSource(cards);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });

    this._accountsService.getAllAccounts().subscribe((accounts) => {
      this._accounts = accounts;
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

  addCard() {
    this._lazyDialog
      .openDialog(
        import('app/dialogs/add-access-card/add-access-card.component'),
        {
          data: this._accounts,
          width: '450px',
        }
      )
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((createdCard: AccessCard) => {
            if (!createdCard) return;

            this.dataSource.data.push(createdCard);

            // trigger table change
            this.dataSource.data = this.dataSource.data;
          })
      );
  }

  deleteCard(card: AccessCard) {
    this._lazyDialog
      .openDialog(
        import('app/dialogs/delete-access-card/delete-access-card.component'),
        {
          data: card,
        }
      )
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((deleted) => {
            if (!deleted) return;
            this.dataSource.data = this.dataSource.data.filter(
              (x) => x.id !== card.id
            );
          })
      );
  }
}
