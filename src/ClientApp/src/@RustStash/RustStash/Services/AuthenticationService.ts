export interface LoginRequest {
  email: string;
  password: string;
}

export interface ForgotPasswordRequest {
  email: string;
}

export interface ResetPasswordRequest {
  email: string;
  newPassword: string;
  token: string;
}

export interface ReinviteAccountRequest {
  email: string;
}

export interface CreateAccountService {
  email: string;
}

export interface ConfirmAccountService {
  fullName: string;
  surname: string;
  emailAddress: string | null;
  password: string;
  token: string;
}

class AuthenticationService {
  public async createAccount(request: CreateAccountService) {
    const result = await fetch('/api/account/create/user', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }

    return result;
  }
  public async reinviteAccount(email: string) {
    const result = await fetch('/api/account/user/reinvite', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(email),
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }

    return result;
  }
  public async login(request: LoginRequest) {
    const result = await fetch('/api/account/login?useCookies=true', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
      credentials: 'include',
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }

    return result;
  }

  public async logout() {
    const result = await fetch('/api/account/logout', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }

    return result;
  }

  public async confirmAccount(request: ConfirmAccountService) {
    const result = await fetch('/api/account/user-confirmation', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }

    return result;
  }

  public async forgotPassword(request: ForgotPasswordRequest) {
    const result = await fetch('/api/account/forgot-password', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }
    return result;
  }

  public async resetPassword(request: ResetPasswordRequest) {
    const result = await fetch('/api/account/reset-password', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    });

    if (!result.ok) {
      const message = await result.text();
      throw new Error(message);
    }
  }
}

export const authenticationService = new AuthenticationService();
