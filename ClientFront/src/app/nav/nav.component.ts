import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = { username: '', password: '' };

  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {
        var currentUrl = this.router.url;
        var refreshUrl =
          currentUrl.indexOf('/members') > -1
            ? '/'
            : '/members';
        this.router
          .navigateByUrl(refreshUrl)
          .then(() => this.router.navigateByUrl(currentUrl));
        
        
        //this.router.navigateByUrl('/members');
      },
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
