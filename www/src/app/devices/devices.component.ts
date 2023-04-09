import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AccessLevel } from 'app/common/models';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { LazyLoadedDialogService } from 'app/common/services/lazy-loaded-dialog.service';
import { DevicesService } from 'app/common/services';
import { DestroyableComponent } from 'app/common/components/destroyable';
import { takeUntil } from 'rxjs';

export interface Device {
  id: string;
  description: string;
  accessLevel: AccessLevel;
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
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.scss'],
})
export default class DevicesComponent
  extends DestroyableComponent
  implements OnInit
{
  displayedColumns: string[] = ['id', 'accessLevel', 'description', 'actions'];
  dataSource: MatTableDataSource<Device>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private _devicesService: DevicesService,
    private _lazyDialog: LazyLoadedDialogService
  ) {
    super();
  }

  ngOnInit(): void {
    this._devicesService.getAllDevices().subscribe((devices) => {
      this.dataSource = new MatTableDataSource(devices);
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

  editDeviceAccessLevel(device: Device) {
    this._lazyDialog
      .openDialog(import('app/dialogs/edit-device/edit-device.component'), {
        data: device,
      })
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((changedDevice: Device) => {
            if (!changedDevice) return;

            // mutate changed device
            this.dataSource.data[
              this.dataSource.data.findIndex(
                (device) => device.id === changedDevice.id
              )
            ] = changedDevice;

            // trigger table change
            this.dataSource.data = this.dataSource.data;
          })
      );
  }

  addDevice() {
    this._lazyDialog
      .openDialog(import('app/dialogs/add-device/add-device.component'))
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((createdDevice: Device) => {
            if (!createdDevice) return;

            this.dataSource.data.push(createdDevice);

            // trigger table change
            this.dataSource.data = this.dataSource.data;
          })
      );
  }

  deleteDevice(device: Device) {
    this._lazyDialog
      .openDialog(import('app/dialogs/delete-device/delete-device.component'), {
        data: device,
      })
      .then((ref) =>
        ref
          .afterClosed()
          .pipe(takeUntil(this.destroy$))
          .subscribe((deleted) => {
            if (!deleted) return;
            this.dataSource.data = this.dataSource.data.filter(
              (x) => x.id !== device.id
            );
          })
      );
  }
}
