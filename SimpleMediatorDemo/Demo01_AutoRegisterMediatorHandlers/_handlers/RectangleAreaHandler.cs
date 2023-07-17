namespace Demo01_AutoRegisterMediatorHandlers;

public sealed class RectangleAreaRequest : ISimpleRequest<AreaResponse>
{
    public double Width  { get; set; }
    public double Height { get; set; }
}

public sealed class AreaResponse
{
    public AreaResponse(double area)
    {
        Area = area;
    }

    public double Area { get; }
}

public class RectangleAreaHandler : ISimpleRequestHandler<RectangleAreaRequest, AreaResponse>
{
    public AreaResponse Handle(RectangleAreaRequest request)
    {
        var area = request.Width * request.Height;
        return new AreaResponse(area);
    }
}
