namespace ChainCalculations;

public class AddDocumentHeader : ICalculator
{
    public void Calculate(DocumentSession session)
    {
        var document = session.ResultDocument;
        if (document.Items.Count == 0)
            return; // nothing to do
        _documentNumber++;
        document.CreateDate = DateOnly.FromDateTime(DateTime.Today);
        document.Number     = _documentNumber.ToString("0000");
        document.Buyer      = session.Person.FullName;
    }

    private int _documentNumber;
}
