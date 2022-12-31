namespace student_service.Models
{
    public class FeeStructure
    {


        public int Id { get; set; }
        
        IEnumerable<FeeStandard> FeeStandards { get; set; }

    }

    public class FeeStandard
    {
        public string StandardName { get; set; }

        public IEnumerable<FeeSubjects> feeSubjects {get;set;}

    }

    public class FeeSubjects
    {


        public int Id { get; set; }
        public string Total { get; set; }


    }


}
