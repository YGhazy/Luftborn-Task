import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { FilterPipe } from 'src/app/services/helpers-and-utilities/Filter.pipe';

@NgModule({
  declarations: [ FilterPipe],
  imports: [
    NgbModule,
    TabsModule,
    FormsModule,
    ToastModule,
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    ConfirmDialogModule,
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
  ],
  exports: [ FilterPipe]
})
export class SharedModule { }
