import boto3
import smtplib
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart

# --- SMTP Configuration ---
SMTP_SERVER = 'webdomain04.dnscpanel.com'       # Your SMTP server address
SMTP_PORT = 465                                 # Your SMTP server port (usually 587 or 465)
SMTP_USERNAME = 'geral@medopt.pt'               # Your SMTP username
SMTP_PASSWORD = '3F*bQw=Tb{,d'                  # Your SMTP password
SENDER_EMAIL = 'geral@medopt.pt'                # Email address to send notifications from

# --- Cognito Configuration ---
USER_POOL_ID = 'us-east-1_6BTN8Ysls'            # Your Cognito User Pool ID
ADMIN_GROUP_NAME = 'admins'                     # Name of the group for admins

cognito_client = boto3.client('cognito-idp')

def get_admins_users():
    # Get all users in the Admins group
    response = cognito_client.list_users_in_group(
        UserPoolId=USER_POOL_ID,
        GroupName=ADMIN_GROUP_NAME
    )
    return response['Users']

def send_email(recipient_email, subject, message_body):
    # Create the email
    msg = MIMEMultipart()
    msg['From'] = SENDER_EMAIL
    msg['To'] = recipient_email
    msg['Subject'] = subject
    
    # Attach the message body
    msg.attach(MIMEText(message_body, 'plain'))
    
    # Send the email using the hard-coded SMTP server values
    with smtplib.SMTP(SMTP_SERVER, SMTP_PORT) as server:
        server.starttls()
        server.login(SMTP_USERNAME, SMTP_PASSWORD)
        server.sendmail(SENDER_EMAIL, recipient_email, msg.as_string())

def send_emails_to_admins(subject, message_body):
    # Get all admins
    admins = get_admins_users()
    
    for admin in admins:
        # Extract the email from admin user attributes
        email = None
        for attribute in admin['Attributes']:
            if attribute['name'] == 'email':
                email = attribute['Value']
                break
        
        if email:
            send_email(email, subject, message_body)

def lambda_handler(event, _):
    # Check if the event indicates that the sign-in was blocked
    if event.get('request', {}).get('userNotFound') or event.get('response', {}).get('signInResult', {}) == 'Blocked':
        # Get the user's email directly from the event
        email = event.get('request', {}).get('userName')  # Assuming the email is passed as the username

        if email:
            # If email is found, send an email to admins
            subject = "User Sign-In Blocked Due to Too Many Failed Attempts"
            message_body = f"""
            <p>The user with email <strong>{email}</strong> has been locked out after exceeding the allowed login attempts.</p>
            <img src="https://imgur.com/BB7Qk6j.png" alt="Signature" />
            """
            send_emails_to_admins(subject, message_body)
    
    return event
