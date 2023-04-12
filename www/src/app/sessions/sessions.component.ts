import { takeUntil } from 'rxjs';
import { AccessCard } from 'app/access-cards/access-cards.component';
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
import { SessionStatus } from 'app/common/models';
import { Device } from 'app/devices/devices.component';
import { LazyLoadedDialogService } from 'app/common/services/lazy-loaded-dialog.service';
import {
  CardsService,
  DevicesService,
  SessionsService,
} from 'app/common/services';

export interface Session {
  id: string;
  userId: string;
  accessCardId: string;
  deviceId: string;
  sessionStatus: SessionStatus;
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
  templateUrl: './sessions.component.html',
  styleUrls: ['./sessions.component.scss'],
})
export default class SessionsComponent
  extends DestroyableComponent
  implements OnInit
{
  private _devices: Device[];
  private _cards: AccessCard[];

  displayedColumns: string[] = [
    'id',
    'userId',
    'accessCardId',
    'deviceId',
    'sessionStatus',
    'actions',
  ];
  dataSource: MatTableDataSource<Session>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _sessionsService: SessionsService,
    private _lazyDialog: LazyLoadedDialogService,
    private _devicesService: DevicesService,
    private _cardsService: CardsService
  ) {
    super();
  }

  ngOnInit(): void {
    this._sessionsService.getAllSessions().subscribe((sessions) => {
      this.dataSource = new MatTableDataSource(sessions);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });

    this._devicesService.getAllDevices().subscribe((devices) => {
      this._devices = devices;
    });

    this._cardsService.getAllCards().subscribe((cards) => {
      this._cards = cards;
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

  addSession() {
    this._lazyDialog
      .openDialog(import('app/dialogs/add-session/add-session.component'), {
        data: { devices: this._devices, cards: this._cards },
        width: '450px',
      })
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((createdSession: Session) => {
            if (!createdSession) return;

            this.dataSource.data.push(createdSession);

            // trigger table change
            this.dataSource.data = this.dataSource.data;
          })
      );
  }

  deleteSession(session: Session) {
    this._lazyDialog
      .openDialog(
        import('app/dialogs/delete-session/delete-session.component'),
        {
          data: session,
          width: '500px',
        }
      )
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((deletedId) => {
            if (!deletedId) return;

            this.dataSource.data[
              this.dataSource.data.findIndex((card) => card.id === deletedId)
            ].sessionStatus = 'Closed';
          })
      );
  }
}
