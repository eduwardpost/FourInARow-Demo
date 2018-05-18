import { Component, OnInit, Input } from '@angular/core';
import { GameService } from '../../services/game.service';
import { Coin } from '../../models/coin';

@Component({
    selector: 'gameboard',
    templateUrl: './gameboard.component.html',
    styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit {

    @Input() aiEnabled: boolean;
    @Input() columns: number;
    @Input() rows: number;

    public board: Coin[][];

    constructor(
        private gameService: GameService
    ) { 
        
    }

    ngOnInit(): void {
        this.board = []
        for(var x: number = 0; x < this.columns; x++) {
            this.board[x] = [];
            for(var y: number = 0; y < this.rows; y++) {
                this.board[x][y] = new Coin(-1, x, y);
            }
        }
     }
}
