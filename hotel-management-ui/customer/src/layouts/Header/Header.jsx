import { Link, NavLink } from "react-router-dom";
import { useSelector } from "react-redux";

import logo from "../../assets/logo.png";
import { routes } from "../../routes";
import UserMenu from "./UserMenu";
import { selectUser } from "../../redux/selectors";

export default function Header() {
	const currentUser = useSelector(selectUser);
	return (
		<>
			{/* top bar */}
			<div className="container-fluid border-top border-bottom px-5 d-none d-lg-block">
				<div className="row">
					<div className="col-lg-6">
						<div className="d-inline-flex">
							<span className="py-3">
								<i className="fa fa-phone text-primary"></i>
								{" (12) 345 67890"}
							</span>
							<span className="mx-3 border-end"></span>
							<span className="py-3">
								<i className="fa fa-envelope text-primary"></i>
								{" info.colorlib@gmail.com"}
							</span>
						</div>
					</div>
					<div className="col-lg-6 text-lg-end d-flex justify-content-end align-items-center">
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
						<div className="d-inline ms-3">
							{currentUser ? (
								<UserMenu />
							) : (
								<>
									<Link to={routes.signIn} className="btn btn-primary">
										Đăng nhập
									</Link>
									<Link to={routes.signUp} className="btn btn-primary ms-3">
										Đăng ký
									</Link>
								</>
							)}
						</div>
					</div>
				</div>
			</div>
			{/* end top bar */}
			{/* nav bar */}
			<div className="container-fluid px-0">
				<nav className="navbar navbar-expand-lg navbar-light px-2 px-lg-5 py-lg-4">
					<a href={routes.home}>
						<img src={logo} alt="logo" />
					</a>
					<button
						className="navbar-toggler collapsed"
						type="button"
						title="show menu"
						data-bs-toggle="collapse"
						data-bs-target="#navbarNav"
						aria-expanded="false">
						<i className="fa fa-bars"></i>
					</button>
					<div className="collapse navbar-collapse" id="navbarNav">
						<ul className="navbar-nav ms-auto p-4 p-lg-0">
							<li className="nav-item">
								<NavLink
									to={routes.home}
									className={({ isActive }) => {
										return `nav-link position-relative p-0 ms-4 ${isActive ? "active" : ""}`;
									}}>
									Home
								</NavLink>
							</li>
							<li className="nav-item">
								<NavLink
									to={routes.rooms}
									className={({ isActive }) => {
										return `nav-link position-relative p-0 ms-4 ${isActive ? "active" : ""}`;
									}}>
									Rooms
								</NavLink>
							</li>
							<li className="nav-item">
								<NavLink
									to={routes.hotel}
									className={({ isActive }) => {
										return `nav-link position-relative p-0 ms-4 ${isActive ? "active" : ""}`;
									}}>
									Hotel
								</NavLink>
							</li>
							<li className="nav-item">
								<NavLink
									to={routes.pages}
									className={({ isActive }) => {
										return `nav-link position-relative p-0 ms-4 ${isActive ? "active" : ""}`;
									}}>
									Pages
								</NavLink>
							</li>
							<li className="nav-item">
								<NavLink
									to={routes.news}
									className={({ isActive }) => {
										return `nav-link position-relative p-0 ms-4 ${isActive ? "active" : ""}`;
									}}>
									News
								</NavLink>
							</li>
							<li className="nav-item">
								<NavLink
									to={routes.contact}
									className={({ isActive }) => {
										return `nav-link position-relative p-0 ms-4 ${isActive ? "active" : ""}`;
									}}>
									Contact
								</NavLink>
							</li>
						</ul>
					</div>
				</nav>
			</div>
			{/* end nav bar */}
		</>
	);
}
