import { Link } from "react-router-dom";
import logo from "../../assets/logo.png";
import { routes } from "../../routes";

export default function Header() {
	return (
		<>
			{/* top bar */}
			<div className="container-fluid border-top border-bottom border-secondary px-5 d-none d-lg-block">
				<div className="row">
					<div className="col-lg-6">
						<div className="d-inline-flex">
							<span className="py-3">
								<i className="fa fa-phone text-primary"></i>
								{" (12) 345 67890"}
							</span>
							<span className="mx-3 border-end border-secondary"></span>
							<span className="py-3">
								<i className="fa fa-envelope text-primary"></i>
								{" info.colorlib@gmail.com"}
							</span>
						</div>
					</div>
					<div className="col-lg-6 text-lg-end">
						<div className="d-inline-flex align-items-center h-100">
							<a href="#" className="me-2">
								<i className="fa-brands fa-facebook"></i>
							</a>
							<a href="#" className="me-2">
								<i className="fa-brands fa-twitter"></i>
							</a>
							<a href="#" className="me-2">
								<i className="fa-brands fa-instagram"></i>
							</a>
						</div>
						<a href="#" className="btn btn-primary ms-3">
							Booking Now
						</a>
						<Link to={routes.signIn} className="btn btn-primary ms-3">
							Sign In
						</Link>
					</div>
				</div>
			</div>
			{/* end top bar */}
			{/* nav bar */}

			{/* end nav bar */}
		</>
	);
}
