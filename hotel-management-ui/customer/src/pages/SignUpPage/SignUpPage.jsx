import { Link } from "react-router-dom";
import { routes } from "../../routes";

export default function SignUpPage() {
	return (
		<div className="container-xxl bg-white d-flex p-0">
			<div className="container-fluid">
				<div className="row h-100 min-vh-100 align-items-center justify-content-center">
					<div className="col-12 col-sm-8 col-md-6">
						{/* should use form tag */}
						<div className="bg-light bg-gradient rounded p-4 p-sm-5 my-4 mx-3">
							<div className="d-flex align-items-center justify-content-between mb-3">
								<Link to={routes.home}>
									<h3 className="text-primary text-uppercase">Home</h3>
								</Link>
								<h3>Sign Up</h3>
							</div>
							<div className="row">
								<div className="col">
									<div className="form-floating mb-3">
										<input className="form-control" id="first-name" placeholder="First Name" />
										<label htmlFor="first-name">First Name</label>
									</div>
									<div className="form-floating mb-3">
										<input className="form-control" id="last-name" placeholder="Last Name" />
										<label htmlFor="last-name">Last Name</label>
									</div>
									<div className="form-floating mb-3">
										<input type="email" className="form-control" id="Username" placeholder="username" />
										<label htmlFor="Username">Username</label>
									</div>
								</div>
								<div className="col">
									<div className="form-floating mb-4">
										<input type="password" className="form-control" id="Password" placeholder="username" />
										<label htmlFor="Password">Password</label>
									</div>
									<div className="form-floating mb-4">
										<input type="password" className="form-control" id="confirm-password" placeholder="username" />
										<label htmlFor="confirm-password">Confirm Password</label>
									</div>
									<div className="d-flex align-items-center justify-content-between mb-4">
										<div className="form-check">
											<input type="checkbox" id="check-password" className="form-check-input" />
											<label htmlFor="check-password" className="form-check-label">
												Check me out!
											</label>
										</div>
									</div>
								</div>
							</div>
							<button type="button" className="btn btn-primary w-100 py-3 mb-4">
								Sign Up
							</button>
							<p className="text-center mb-0">
								{"Already have an Account? "}
								<Link to={routes.signIn}>Sign In</Link>
							</p>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
