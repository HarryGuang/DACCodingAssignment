import { Component, OnInit } from '@angular/core';
import { MembersService } from 'src/app/_services/members.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-useredit-modal',
  templateUrl: './useredit-modal.component.html',
  styleUrls: ['./useredit-modal.component.css'],
})
export class UsereditModalComponent implements OnInit {
  userid = 0;
  username = '';
  displayName = '';
  firstName = '';
  lastName = '';
  gender = '';
  closeBtnName = '';

  userEditForm: FormGroup = new FormGroup({});

  genderList = [
    { value: 'male', display: 'Males' },
    { value: 'female', display: 'Females' },
  ];

  constructor(
    private membersService: MembersService,
    public bsModalRef: BsModalRef,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.userEditForm = this.fb.group({
      gender: [this.gender],
      username: [this.username, Validators.required],
      displayName: [this.displayName, Validators.required],
      firstName: [this.firstName, Validators.required],
      lastName: [this.lastName, Validators.required],
    });
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { notMatching: true };
    };
  }

  updateMember() {
    const values = this.userEditForm.value;
    console.log(values);

    this.membersService.updateMember(this.userid, values).subscribe({
      next: () => {
        this.membersService.memberCache = new Map();
        this.bsModalRef.hide();

        var currentUrl = this.router.url;
        var refreshUrl = currentUrl.indexOf('/members') > -1 ? '/' : '/members';
        this.router
          .navigateByUrl(refreshUrl)
          .then(() => this.router.navigateByUrl(currentUrl));
        //this.router.navigateByUrl('/members');
        this.toastr.success('User updated');
      },
      error: (error) => {
        this.toastr.error(error.error);
      },
    });
  }
}
