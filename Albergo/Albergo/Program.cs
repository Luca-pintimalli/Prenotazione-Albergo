﻿using Albergo.Models;
using Albergo.Models.Camere;
using Albergo.Services;
using Albergo.Services.Auth;
using Albergo.Services.CLIENTI;
using Albergo.Services.Servizi;
using Albergo.Services.SERVIZI;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICamereService, CamereService>();
builder.Services.AddScoped<IClientiService, ClientiService>();
builder.Services.AddScoped<IServiziService, ServiziService>();
builder.Services.AddScoped<IPrenotazioniService, PrenotazioniService>();
builder.Services.AddScoped<PrenotazioneForm>();
builder.Services.AddScoped<IServiziPrenotazioneService, ServiziPrenotazioneService>();
builder.Services.AddScoped<ServizioPrenotazioneForm>();



//CONF AUTENTICAZIONE
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Account/Login";
    });

//CONF SERVIZIO DI GESTIONE AUTENTICAZIONE
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

