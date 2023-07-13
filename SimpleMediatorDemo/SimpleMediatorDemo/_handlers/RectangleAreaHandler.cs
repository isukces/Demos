namespace SimpleMediatorDemo;

public sealed class RectangleAreaRequest : ISimpleRequest<AreaResponse>
{
    #region Properties

    public double Width  { get; set; }
    public double Height { get; set; }

    #endregion
}

public sealed class AreaResponse
{
    public AreaResponse(double area)
    {
        Area = area;
    }

    #region Properties

    public double Area { get; }

    #endregion
}

public class RectangleAreaHandler : ISimpleRequestHandler<RectangleAreaRequest, AreaResponse>
{
    public AreaResponse Handle(RectangleAreaRequest request)
    {
        var area = request.Width * request.Height;
        return new AreaResponse(area);
    }
}
