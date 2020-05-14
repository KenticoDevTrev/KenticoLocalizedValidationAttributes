# Other Helpful Attributes Not in Assembly
Here are a couple Validation Attributes that you can leverage in your Kentico site, revolving around User Creation / management.

These are featured on the Baseline project (may not be up immediately).

Keep in mind that these attributes leverage a couple of Repository and Service implementations that you yourself will need to create, again you can reference the baseline project for my implementations of them.

# Attributes
## LocalizedPasswordPolicyAttribute
This leverages Kentico's password policy settings and applies this validation to the field.  It will automatically create the Client side and server side validations based on this setting.

## UserDoesntExistAttribute / UserExistsAttribute
Useful for User registration to prevent a new user from using the same username/email as a current user (UserDoesntExistsAttribute), or in forms where you need to select an existing user for some operation (UserExistsAttribute).

## CurrentUserPasswordValidAttribute
This is useful for Password resets for the current logged in user.  This checks if the "Current Password" field matches the current logged in user's password.  Most password change UIs require the logged in user to verify their password before letting them change it.