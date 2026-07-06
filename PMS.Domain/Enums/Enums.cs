namespace PMS.Domain.Enums
{
    public enum LeaveType
    {
        Annual_Leave_Paid = 1,
        Annual_Leave_UnPaid = 2,
        AL_Maternity_Paid = 3,
        AL_Maternity_Unpaid = 4,
        AL_Paternity_Paid = 5,
        AL_Paternity_Unpaid = 6,
        AL_EPL_Paid = 7,
        AL_EPL_Unpaid = 8,
        AL_LOA_Paid = 9,
        AL_LOA_Unpaid = 10,
        AL_HL_Paid = 11,
        CL_Casual_Leave = 12,
        CL_Pre_Approved_absence_Casual_Leave_Call_Out = 13,
        CL_Post_Approved_And_Unpaid_Call_Out = 14,
        CL_Pre_Approved_Sick_Callout = 15,
        CL_Post_approved_unpaid_Sick_Callout = 16,
    }
    public enum Approval
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
    }

    public enum LoanApproveStatus
    {
        Pending = 1,
        SupervisorApproved = 2,
        DirectorApproved = 3,
        Rejected = 4,
        Released = 5,
    }

    public enum LoanInstallment
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
    }
    public enum Active
    {
        Active = 1,
        InActive = 2,
    }
    public enum JobStatus
    {
        Permanent = 1,
        Temporary = 2,
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
    }
    public enum SalaryType
    {
        Daily = 1,
        Weekly = 2,
        Fortnightly = 3,
        Monthly = 4,
    }

    public enum Legends
    {
        P = 1,
        OFF = 2,
        T = 3,
        TT = 4,
        SHA = 5, 
        EL = 6, 
        CL = 7, 
        U_CL = 8, 
        SL = 9, 
        U_SL = 10,   
        NCNS = 11, 
        ML = 12, 
        AL = 13, 
        TM = 14, 
        RS = 15, 
        SU = 16, 
        TD = 17, 
        OT = 18, 
        CC = 19, 
    }

    public enum Action
    {
        VCA = 1,
        FCA,
        WCA,
        CA
    }

    public enum Permission
    {
        User_Management = 1,
        Application_Users,
        User_Roles,
        User_Permissions,
        Administration,
        Team_Structure,
        Add_New_Employee,
        View_Employee_Detail,
        Edit_Employee_Information,
        Department,
        Add_New_Department,
        Designation,
        Add_New_Designation,
        Hierarchy,
        Leave,
        Apply_Leave,
        Leave_Status,
        Team_Pending_Leaves,
        Team_Approved_Leaves,
        Loan,
        Loan_Request,
        Loan_Status,
        Team_Pending_Loan,
        Final_Pending_Loan,
        Released_Approved_Loan,
        Polices,
        Policiy_Documents,
        Score_Card,
        Score_Card_Documents
    }
}
