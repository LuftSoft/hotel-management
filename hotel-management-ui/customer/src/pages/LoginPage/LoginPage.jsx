export default function LoginPage() {
	return (
		<div className="container-xxl bg-white d-flex p-0">
			<div className="container-fluid">
				<div className="row h-100 min-vh-100 align-items-center justify-content-center">
					<div className="col-12 col-sm-8 col-md-6 col-lg-5 col-xl-4">
						{/* should use form tag */}
						<div className="bg-light bg-gradient rounded p-4 p-sm-5 my-4 mx-3">
							<div className="d-flex align-items-center justify-content-between mb-3">
								<a href="/">
									<h3 className="text-primary text-uppercase">Home</h3>
								</a>
								<h3>Sign In</h3>
							</div>
							<div className="form-floating mb-3">
								<input className="form-control" id="Username" placeholder="username" />
								<label htmlFor="Username">Username</label>
							</div>
							<div className="form-floating mb-4">
								<input type="password" className="form-control" id="Password" placeholder="username" />
								<label htmlFor="Password">Password</label>
							</div>
							<div className="d-flex align-items-center justify-content-between mb-4">
								<div className="form-check">
									<input type="checkbox" id="check-password" className="form-check-input" />
									<label htmlFor="check-password" className="form-check-label">
										Check me out!
									</label>
								</div>
								<a href="#">Forgot Password</a>
							</div>
							<button type="button" className="btn btn-primary w-100 py-3 mb-4">
								Sign In
							</button>
							<p className="text-center mb-0">
								{"Don't have an Account? "}
								<a href="#">Sign Up</a>
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
