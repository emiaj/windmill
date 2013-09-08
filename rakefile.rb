begin
  require 'bundler/setup'
  require 'fuburake'
  require 'zip'

end

props = {
	:artifacts => File.expand_path("artifacts"),
	:root => File.dirname(__FILE__),
}

@solution = FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/Windmill.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "Windmill",
		:copyright => 'Jaime Febres'
	}

	sln.ripple_enabled = true
end