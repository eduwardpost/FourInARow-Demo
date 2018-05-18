import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { GameboardComponent } from './components/gameboard/gameboard.component';
import { GameService } from './services/game.service';
import { CoinComponent } from './components/coin/coin.component';

@NgModule({
    declarations: [
        AppComponent,
        GameboardComponent,
        CoinComponent
    ],
    providers: [
        GameService
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
    ]
})
export class AppModuleShared {
}
