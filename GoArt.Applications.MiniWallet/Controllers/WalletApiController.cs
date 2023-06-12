using System.Net;
using GoArt.Applications.MiniWallet.Controllers.ApiModels;
using GoArt.Applications.MiniWallet.Core.Problem;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Features.AddWallet;
using GoArt.Applications.MiniWallet.Features.Deposit;
using GoArt.Applications.MiniWallet.Features.GetBalance;
using GoArt.Applications.MiniWallet.Features.GetMoneyTransactionReports;
using GoArt.Applications.MiniWallet.Features.GetWalletById;
using GoArt.Applications.MiniWallet.Features.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoArt.Applications.MiniWallet.Controllers;

[ApiController]
[Route("api/wallets")]
public class WalletApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalletApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(GetWalletByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetWalletByIdResponse>> GetWalletById([FromRoute] string id)
    {
        GetWalletByIdRequest request = new GetWalletByIdRequest(WalletId.Create(id));
        GetWalletByIdResponse response = await _mediator.Send(request);
        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddWalletResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AddWalletResponse>> CreateWallet(AddWalletApiRequest apiRequest)
    {
        AddWalletRequest request = new AddWalletRequest(apiRequest.WalletName!);
        AddWalletResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("{walletId}/deposit")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(DepositResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DepositResponse>> Deposit([FromRoute] Guid walletId, DepositApiRequest apiRequest)
    {
        DepositRequest depositRequest = new DepositRequest(
            WalletId.Create(walletId),
            new MoneyAmountWithCurrency(
                new MoneyAmount(apiRequest.WholePart, apiRequest.PennyPart),
                Currency.Create(apiRequest.Currency)));

        DepositResponse depositResponse = await _mediator.Send(depositRequest);
        return Ok(depositResponse);
    }

    [HttpPost("{walletId}/withdraw")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(WithdrawResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<WithdrawResponse>> Withdraw([FromRoute] Guid walletId, WithdrawApiRequest apiRequest)
    {
        WithdrawRequest withdrawRequest = new WithdrawRequest(
            WalletId.Create(walletId),
            new MoneyAmountWithCurrency(
                new MoneyAmount(apiRequest.WholePart, apiRequest.PennyPart),
                Currency.Create(apiRequest.Currency)));

        WithdrawResponse withdrawResponse = await _mediator.Send(withdrawRequest);
        return Ok(withdrawResponse);
    }

    [HttpGet("{walletId}/balance/{currency}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetBalanceResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetBalanceResponse>> GetBalance([FromRoute] Guid walletId, [FromRoute] string currency)
    {
        GetBalanceRequest balanceRequest = new GetBalanceRequest(Currency.Create(currency), WalletId.Create(walletId));
        GetBalanceResponse balanceResponse = await _mediator.Send(balanceRequest);
        return Ok(balanceResponse);
    }

    [HttpGet("{walletId}/transactions")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetMoneyTransactionsReponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetMoneyTransactionsReponse>> GetTransactions([FromRoute] Guid walletId)
    {
        GetMoneyTransactionsRequest moneyTransactionsRequest = new GetMoneyTransactionsRequest(WalletId.Create(walletId));
        GetMoneyTransactionsReponse moneyTransactionsReponse = await _mediator.Send(moneyTransactionsRequest);
        return Ok(moneyTransactionsReponse);
    }
}