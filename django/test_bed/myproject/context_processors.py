import os

def get_stage_env(request):
    return { 'STAGE': os.environ['STAGE'] }