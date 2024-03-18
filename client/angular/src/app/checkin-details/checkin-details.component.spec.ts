import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckinDetailsComponent } from './checkin-details.component';

describe('CheckinDetailsComponent', () => {
  let component: CheckinDetailsComponent;
  let fixture: ComponentFixture<CheckinDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CheckinDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CheckinDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
