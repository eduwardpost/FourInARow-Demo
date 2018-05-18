import { Component, OnInit, Input } from '@angular/core';
import { GameService } from '../../services/game.service';
import { Coin } from '../../models/coin';

@Component({
    selector: 'coin',
    templateUrl: './coin.component.html',
    styleUrls: ['./coin.component.css']
})
export class CoinComponent implements OnInit {

    @Input() coin: Coin;

    public board: Coin[][];

    constructor(
        private gameService: GameService
    ) { 
        
    }

    ngOnInit(): void {
        
     }
}
