﻿namespace WC.Service.PersonalData.gRPC.Client.Models.Verify;

public class VerifyCredentialsRequestModel
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
