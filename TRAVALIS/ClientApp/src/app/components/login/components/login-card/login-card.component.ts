import { Component, HostListener, ViewChild, AfterViewInit, ViewChildren } from '@angular/core';
import { Router } from '@angular/router';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { NgForm } from '@angular/forms';
import { UtilitiesService } from 'src/app/services/utilities/utilities.service';
import { CoreCacheService } from '../../../../core/cache/core-cache.service';

@Component({
  selector: 'uvepm-login-card',
  templateUrl: './login-card.component.html',
  styleUrls: ['./login-card.component.css'],
  animations: [
    trigger(
      'myAnimation',
      [
        state('true', style({ opacity: 1 })),
        state('false', style({ opacity: 0.3 })),
        transition('1 => 0', animate('900ms', style({ opacity: 0.3 }))),
        transition('0 => 1', animate('900ms', style({ opacity: 1 })))
      ])
  ]
})
export class LoginCardComponent implements AfterViewInit {

  @ViewChildren('userName') userNameElement;
  @ViewChildren('password') passwordElement;

  backgrounds = ['/assets/img/back_01', '/assets/img/back_02', '/assets/img/back_03', '/assets/img/back_04', '/assets/img/back_05'];
  backgroundIndex = Math.floor(Math.random() * this.backgrounds.length) + 0;
  secondsToChange = 50;
  pause = false;
  activeBackgroundImage = '';
  showBackgroundImage = false;

  // Form
  invalidUser = false;

  ENTER_KEYCODE = 13;

  @HostListener('document:keydown', ['$event']) onKeydownHandler(event: KeyboardEvent) {
    if (event.keyCode === this.ENTER_KEYCODE) {
      this._focusOnElementInput(this.userNameElement);
      this._focusOnElementInput(this.passwordElement);
      setTimeout(() => {
        this._login();
      }, 100);
    }
  }



  constructor(
    private _router: Router,
    private _utilitiesServices: UtilitiesService,
    private _coreCacheService: CoreCacheService
  ) {
    this.activeBackground(false);
    setInterval(() => { this._onChangeBackground(); }, 600 * this.secondsToChange);
  }

  public ngAfterViewInit() {
    setTimeout(() => this._focusOnElementInput(this.userNameElement), 200);
  }

  private _focusOnElementInput(element: any) {
    element.first._elementRef.nativeElement.children[0].children[1].children[0].children[0].children[0].children[0].children[0].children[0].focus();
  }

  public loginSubmit(loginForm: NgForm) {
    if (loginForm.valid) {
      this._login();
    }
  }


  public verifyTwoWayFactor(){
    // if(this.loginRequestDto.twoWayFactorCode !== null)
    //   this._login();
    // else
    // alert('Inserte el código');
  }

  public goBack(){
    this.invalidUser = false;
  }
  private _login() {
    // this.loginRequestDto.deviceId = this._coreCacheService.getByKey('deviceId');
    // this._authService.login(this.loginRequestDto).subscribe(
    //   userLogonDto => this._logInIsValid(userLogonDto),
    //   () => this._logInIsNotValid()
    // ),
    // error =>{
    //   console.log(error);
    // };
  }

  private _logInIsNotValid() {
    this.invalidUser = true;
  }

  private _logInIsValid(userLogonDto) {
    // this._coreCacheService.setByKey('UserLogonDto', userLogonDto);
    // if (userLogonDto.exeviResponse.dispositivo != null)
    //   this._coreCacheService.setByKey('deviceId', userLogonDto.exeviResponse.dispositivo, { maxTime: 1296000 });
    // if(userLogonDto.appraiser.key == null && userLogonDto.exeviResponse.aplicaDobleFactor == 'S'){
    //   this.loginRequestDto.isTwoWayFactor = true;
    //   this.loginRequestDto.exeviToken = userLogonDto.exeviResponse.access_Token;
    //   return;
    // }
    // if (this._webCacheService.IsValidCache()) {
    //   this._redirectToSearchFilter();
    // } else {
    //   this._utilitiesServices.refreshCache(this._cacheService, this._globalCacheService)
    //     .subscribe(result => {
    //       this._webCacheService.SaveAllDataCacheFromCommon(result[0]);
    //       this._webCacheService.SaveAllDataCacheFromPM(result[1]);
    //       this._redirectToSearchFilter();
    //     });
    // }
  }

  private _redirectToSearchFilter() {
    this._router.navigate(['/initial/search']);
  }

  public activeBackground(withblur) {
    this.showBackgroundImage = false;
    let back = this.backgrounds[this.backgroundIndex];
    if (withblur) { back += '_blur'; }
    setTimeout(() => {
      this.activeBackgroundImage = back += '.jpg';
      this.showBackgroundImage = true;
    }, 1000);
  }

  public bluringBackground() {
    this.activeBackground(true);
    this.pause = true;
  }

  public unbluringBackground() {
    this.pause = false;
    this.activeBackground(false);
  }

  private _onChangeBackground() {
    if (!this.pause) {
      this.backgroundIndex++;
      if (this.backgroundIndex >= this.backgrounds.length) {
        this.backgroundIndex = 0;
      }
      this.activeBackground(false);
    }
  }
}
