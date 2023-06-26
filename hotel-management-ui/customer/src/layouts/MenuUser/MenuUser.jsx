import SidebarUser from "../../components/SidebarUser";
import Footer from "../Footer";
import Header from "../Header";

export default function MenuUser({ children }) {
	return (
		<>
			<Header />
			<div>
				{/* body */}
				<div className="bg-light">
					<div className="Container">
						<div className="d-flex flex-column">
							<div className="d-flex gap-4 my-3">
								<SidebarUser />
								<div className="flex-grow-1 d-flex flex-column gap-3">
									{/* here */}
									{children}
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
			{/* Footer */}
			<Footer />
		</>
	);
}
