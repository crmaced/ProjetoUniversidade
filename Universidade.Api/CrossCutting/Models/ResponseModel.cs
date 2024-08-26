﻿namespace Universidade.Api.CrossCutting.Models;

public class ResponseModel
{
    public int Code { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
}