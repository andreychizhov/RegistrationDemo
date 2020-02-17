import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AlertService, UserService, AuthenticationService } from '@/_services';
import { LocationService } from '@/_services/location.service';

@Component({ templateUrl: 'step2.component.html' })
export class Step2Component implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    country = [];
    province = [];

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private locationService: LocationService,
        private alertService: AlertService
    ) {
        // redirect to home if already logged in
        if (this.authenticationService.currentUserValue) {
            this.router.navigate(['/']);
        }

        this.locationService.getCountries().subscribe(data => {
            this.country = data;
        },
        error => {
            this.alertService.error(error);
        });
    }

    filterProvinces(filterVal: any) {
        this.locationService.getProvinces(parseInt(filterVal)).subscribe(data => {
            this.province = data;
        },
        error => {
            this.alertService.error(error);
        });
    }

    ngOnInit() {
        if (history.state.data == undefined)
        {
            this.router.navigate(['/login']);
        }

        this.registerForm = this.formBuilder.group({
            country: ['', Validators.required],
            province: ['', [Validators.required]]
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        
        this.loading = true;
        let req = history.state.data;
        req.countryId = parseInt(this.registerForm.value.country);
        req.provinceId = parseInt(this.registerForm.value.province);

        this.userService.register(req)
            .pipe(first())
            .subscribe(
                data => {
                    this.alertService.success('Registration successful!', true);
                    this.router.navigate(['/login']);
                },
                error => {
                    this.alertService.error(error);
                    this.loading = false;
                });
    }
}
