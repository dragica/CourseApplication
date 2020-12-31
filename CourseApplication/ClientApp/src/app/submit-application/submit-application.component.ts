import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators, FormArray } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common'

import { ApiService } from '../api.service';
import { Course } from '../course';
import { CourseDate } from '../coursedate';
import { Participant } from '../participant';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-submit-application',
  templateUrl: './submit-application.component.html',
  styleUrls: ['./submit-application.component.css']
})
export class SubmitApplicationComponent implements OnInit {

  applicationForm: FormGroup;
  courses: Course[] = [];
  courseDates: CourseDate[] = [];
  participants: Participant[] = [];
  companyName = '';
  companyPhone = '';
  companyEmail = '';
  participantName = '';
  participantPhone = '';
  participantEmail = '';
  selectedCourse: number = null;
  selectedDate: string = null;
  matcher = new MyErrorStateMatcher();

  constructor(private api: ApiService, private formBuilder: FormBuilder, private router: Router, public datepipe: DatePipe) { }

  ngOnInit() {

    this.api.getCourses()
      .subscribe((res: Course[]) => {
        this.courses = res;
        console.log(this.courses);
      }, err => {
        console.log(err);
      });

    this.applicationForm = this.formBuilder.group({
      companyName: [null, Validators.required],
      companyPhone: [null, Validators.required],
      companyEmail: [null, Validators.required],
      selectedCourse: [null, Validators.required],
      selectedDate: [null, Validators.required],
      participants: this.formBuilder.array([])
    });

  }

  getCourseDates() {

    this.api.getCourseDates(this.selectedCourse)
      .subscribe((res: CourseDate[]) => {
        this.courseDates = res;
        console.log(this.courseDates);
      }, err => {
        console.log(err);
      });
  }

  addParticipant() {

    let participant = new Participant();
    participant.FullName = this.participantName;
    participant.Email = this.participantEmail;
    participant.Phone = this.participantPhone;

    this.participants.push(participant);

    let participantFormControl = this.applicationForm.get('participants');
    (participantFormControl as FormArray).push(this.formBuilder.group({
      FullName: this.participantName,
      Phone: this.participantPhone,
      Email: this.participantEmail
    }));

    this.participantName = '';
    this.participantPhone = '';
    this.participantEmail = '';
  }

  onFormSubmit() {

    this.api.submitApplication(this.applicationForm.value)
      .subscribe((res: any) => {
        console.log(res);
        if (res.success)
          this.router.navigate(['/submit-success']);
        else
          alert(res.message);
        console.log(res.message);
      }, (err: any) => {
        console.log(err);
      });
  }

}
