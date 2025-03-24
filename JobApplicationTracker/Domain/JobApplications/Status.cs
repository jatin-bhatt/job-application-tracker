namespace Domain.JobApplications {
    /// <summary>
    /// Status of Job Application
    /// 
    /// Submitted => Application has been received but not yet reviewed.
    /// UnderReview => The hiring team is evaluating your application.
    /// Shortlisted => Profile has been selected for further consideration.
    /// InterviewScheduled =>  Invited for an interview.
    /// InterviewCompleted => Finished the interview process and are awaiting a decision.
    /// OfferExtended => Received a job offer.
    /// Hired => Accepted the job offer and completed onboarding.
    /// Rejected => Application was not selected.
    /// OnHold => The employer has paused the hiring process or is considering other candidates.
    /// </summary>
    public enum Status {
        Submitted = 0,
        UnderReview = 1,
        Shortlisted = 2,
        InterviewScheduled = 3,
        InterviewCompleted = 4,
        OfferExtended = 5,
        Hired = 6,
        Rejected = 7,
        OnHold = 8
    }
}
