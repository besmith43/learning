# myapp/views.py
from django.shortcuts import render
from django.http import HttpResponse
from .models import Post

def home(request):
    context = {
        'role': 1
    }
    return render(request, 'home.html', context)

def about(request):
    return HttpResponse('<h1>About Page</h1>')

def contact(request):
    return HttpResponse('<h1>Contact Page</h1>')