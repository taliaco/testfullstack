import { Component, OnInit, Inject} from '@angular/core';
import { LocationService } from '../services/location.service';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  location: any;
  displayedColumns: string[] = [];
  savedLocation: any[];

  constructor(private locationSvc: LocationService,
    public dialog: MatDialog) {
    this.displayedColumns = [
      'timestamp',
      'latitude',
      'longitude',
      'note'
    ];
  }
  ngOnInit() {
    var that = this;
    that.locationSvc.getLocation().subscribe(
      (location: any) => {
        that.location = location;
      },
      error => {
        console.log(error);
      }
    );

    setInterval(function () {
      that.locationSvc.getLocation().subscribe(
        (location: any) => {
          that.location = location;
        },
        error => {
          console.log(error);
        }
      );
    }, 2000);

    this.locationSvc.getSavedLocations().subscribe(
      (locations: any) => {
        that.savedLocation = locations;
      },
      error => {
        console.log(error);
      }
    );
  }

  onSavePress(currLocation: any) {
    var that = this;
    this.locationSvc.saveLocations(currLocation).subscribe(
      (locations: any) => {
        that.savedLocation = locations;
      },
      error => {
        console.log(error);
      }
    );
  }
  openDialog(currLocation: any): void {
    const dialogRef = this.dialog.open(SaveLocationDialog, {
      width: '250px',
      data:  currLocation ,
    });
    var that = this;

    dialogRef.afterClosed().subscribe(result => {
      that.onSavePress(result);
    });
  }
}
  @Component({
    selector: 'saveLocationDialog',
    templateUrl: 'saveLocationDialog.html',
  })
  export class SaveLocationDialog {
  constructor(
    public dialogRef: MatDialogRef<SaveLocationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
