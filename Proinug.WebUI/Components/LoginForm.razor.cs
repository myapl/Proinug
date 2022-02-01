﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using HigLabo.Core;
using Proinug.WebUI.Models;
using Proinug.WebUI.Services;
using Proinug.WebUI.ViewModels;

namespace Proinug.WebUI.Components;

public partial class LoginForm
{
    [Inject]
    public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    
    private CredentialsVm _credentials = new();
    private bool _busy;
    private int _errorCode;
    private string _errorMessage = "";

    private async Task LoginAsync()
    {
        _errorCode = 0;
        
        if (AuthenticationStateProvider == null) return;
        var asp = (CwAuthenticationStateProvider) AuthenticationStateProvider;

        _busy = true;
        _errorCode = await asp.LoginAsync(_credentials.Map(new Credentials()));

        if (_errorCode == 401)
        {
            _errorMessage = "Wrong username or password.";
            _busy = false;
            return;
        }
        
        if (_errorCode != 0)
        {
            _errorMessage = "Something went wrong while login.";
        }
        _busy = false;
    }
}