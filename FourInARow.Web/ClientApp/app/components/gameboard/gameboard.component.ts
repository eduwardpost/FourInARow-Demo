import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { GameService } from '../../services/game.service';
import { Coin } from '../../models/coin';

@Component({
    selector: 'gameboard',
    templateUrl: './gameboard.component.html',
    styleUrls: ['./gameboard.component.css']
})
export class GameboardComponent implements OnInit, OnChanges {

    @Input() aiEnabled: boolean;
    @Input() columns: number;
    @Input() rows: number;

    public board: Coin[][];

    constructor(
        private gameService: GameService
    ) { 
        
    }

    ngOnInit(): void {
       
     }

     setBoard() {
        this.board = []
        for(var x: number = 0; x < this.columns; x++) {
            this.board[x] = [];
            for(var y: number = 0; y < this.rows; y++) {
                this.board[x][y] = new Coin(-1, x, y);
            }
        }
     }

     public setOwner(coin: Coin, owner: number) {
        coin.owner = owner;
     }

     ngOnChanges(changes: SimpleChanges) {
        this.setBoard();
      }
      
}
