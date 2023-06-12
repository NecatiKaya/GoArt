using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.GetWalletById;

public class GetWalletByIdRequest : IRequest<GetWalletByIdResponse>
{
    public WalletId Id { get; set; }

    public GetWalletByIdRequest(WalletId id)
    {
        Id = id;
    }
}