namespace Domain.JobApplications {
    /// <summary>
    /// Job Application Entity
    /// </summary>
    public class JobApplication {
        /// <summary>
        /// Unique Identifier determining Job Application
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of Company for which Job Application was made.
        /// </summary>
        public required string CompanyName { get; set; }
        /// <summary>
        /// Position for which Job application was made.
        /// </summary>
        public required string Position { get; set; }
        /// <summary>
        /// Status of Job Application
        /// </summary>
        public required Status Status { get; set; }
        /// <summary>
        /// Date at which Job Application was made.
        /// </summary>
        public required DateTime ApplicationDate { get; set; }
    }
}
