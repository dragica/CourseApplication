import { ComponentFactory } from "@angular/core";
import { Company } from "./company";
import { Participant } from "./participant";

export class CourseApplication {
    CourseId: number;
    Date: string;
    Participants: Participant[];
    ParticipantCompany: Company;
    
  }
