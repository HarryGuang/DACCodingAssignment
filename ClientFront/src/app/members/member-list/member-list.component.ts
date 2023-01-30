import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { UserParams } from 'src/app/_models/userParams';
import { MembersService } from 'src/app/_services/members.service';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { User } from '../../_models/user';
import { ToastrService } from 'ngx-toastr';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { UsereditModalComponent } from 'src/app/modals/useredit-modal/useredit-modal.component';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  members: Member[] = [];
  pagination: Pagination | undefined;
  userParams: UserParams | undefined;
  genderList = [
    { value: 'male', display: 'Males' },
    { value: 'female', display: 'Females' },
  ];
  user: User = {} as User;
  canEdit = false;
  canDelete = false;
  bsModalRef: BsModalRef<UsereditModalComponent> =
    new BsModalRef<UsereditModalComponent>();

  constructor(
    private accountService: AccountService,
    private membersService: MembersService,
    private toastr: ToastrService,
    private modalService: BsModalService
  ) {
    this.userParams = this.membersService.getUserParams();
    //console.log(this.userParams);
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }

  ngOnInit(): void {
    this.loadMembers();
    if (this.user != null) {
      var appHasRole = "['Admin', 'Moderator']";
      if (this.user.roles.some((r) => appHasRole.includes(r))) {
        this.canEdit = true;
        this.canDelete = true;
      }
    }
  }

  loadMembers() {
    //console.log(this.userParams);
    if (this.userParams) {
      this.membersService.setUserParams(this.userParams);
      this.membersService.getMembers(this.userParams).subscribe({
        next: (Response) => {
          if (Response.result && Response.pagination) {
            this.members = Response.result;
            this.pagination = Response.pagination;
          }
        },
      });
    }
  }

  resetFilters() {
    this.userParams = this.membersService.resetUserParams();
    this.loadMembers();
  }

  pageChanged(event: any) {
    if (this.userParams && this.userParams?.pageNumber !== event.page) {
      this.userParams.pageNumber = event.page;
      this.membersService.setUserParams(this.userParams);
      this.loadMembers();
    }
  }

  deleteUser(id: number) {
    if (confirm('Are you sure to delete this user?')) {
      this.membersService.deleteMember(id).subscribe({
        complete: () => {
          this.toastr.success('User deleted.');

          this.membersService.memberCache = new Map();
          this.loadMembers();

          if (this.user != null) {
            var appHasRole = "['Admin', 'Moderator']";
            if (this.user.roles.some((r) => appHasRole.includes(r))) {
              this.canEdit = true;
              this.canDelete = true;
            }
          }
        },
        error: (error) => {
          this.toastr.error(error.error, error.status.toString());
        },
      });
    }
  }

  openUserEditModal(user: Member) { 
    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        userid: user.id,
        username: user.userName,
        displayName: user.displayName,
        firstName: user.firstName,
        lastName: user.lastName,
        gender: user.gender,
        closeBtnName: 'Update',
        title: 'Edit User'
      },
    };
    this.bsModalRef = this.modalService.show(UsereditModalComponent, config);

    this.bsModalRef.onHide?.subscribe({
      next: () => { 
        /*
        const selectedRoles = this.bsModalRef.content?.selectedRoles;
        if (!this.arrayEqual(selectedRoles!, user.roles)) { 
          this.adminService
            .updateUserRoles(user.userName, selectedRoles!.toString())
            .subscribe({
              next: (roles) => (user.roles = roles),
            });
        }
        */
      }
    })
  }
}
