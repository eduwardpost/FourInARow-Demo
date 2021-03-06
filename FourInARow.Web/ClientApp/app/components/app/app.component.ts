import { Component } from '@angular/core';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public playerLabel: string = 'none';
    public aiEnabled: boolean = true;
    public columns: number =0;
    public rows: number = 0;

    public startgame(columnsInput: number, rowsInput: number) {
        this.columns = columnsInput;
        this.rows = rowsInput;
    }
}
