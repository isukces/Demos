using Common;

namespace SomeVisualLib.InvoiceMaker;

public class InvoiceMakerImplementation:IInvoiceMaker
{
    public void MakeInvoice()
    {
        InvoiceWindow window = new InvoiceWindow();
        window.ShowDialog();
    }
}
